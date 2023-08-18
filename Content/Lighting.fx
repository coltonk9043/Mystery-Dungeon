#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

#define MAX_LIGHTS 32
#define PIXEL_SIZE 6.0f

// Ambient Lighting
float4 DiffuseLighting;

// Light 1
float3 LightPosition[MAX_LIGHTS];
float4 LightColour[MAX_LIGHTS];
float LightIntensity[MAX_LIGHTS];
float LightRadius[MAX_LIGHTS];
int LightCount;

// Screen texture that we are overlaying onto.
Texture2D ScreenTexture;
sampler2D ScreenTextureSampler = sampler_state
{
	Texture = <ScreenTexture>;
};

// Mask texture (Walls)
Texture2D MaskTexture;
sampler2D MaskTextureSampler = sampler_state
{
	Texture = <MaskTexture>;
};

SamplerState SampleType;

struct VertexShaderOutput
{
	float2 Position : SV_POSITION0;
	float4 Colour : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float initialAlpha = input.Colour.a;
	
    float4 resultingColor = float4(0.0f, 0.0f, 0.0f, 0.0f);
	
    for (int i = 0; i < LightCount; i++)
    {
        float2 flooredPosition = floor(input.Position / PIXEL_SIZE) * PIXEL_SIZE;
        float3 flooredLightPosition = floor(LightPosition[i] / PIXEL_SIZE) * PIXEL_SIZE;
		
        float lightDistance = pow(flooredPosition.x - flooredLightPosition.x, 2) + pow(flooredPosition.y - flooredLightPosition.y, 2);

        float4 textureColour;
        float4 lightColor;
		
		// Determine the diffuse color amount of each light.
        lightColor = (LightColour[i] * LightIntensity[i]) * max(0, (1 - lightDistance / (LightRadius[i] * 64)));
        resultingColor += lightColor;
    }

    float4 textureColour = ScreenTexture.Sample(SampleType, input.TextureCoordinates);

    float4 colour = saturate(DiffuseLighting + resultingColor) * textureColour;
	
	// Uses a mask in order to replicate raycasting.
	//float4 maskColor = tex2D(MaskTextureSampler, input.TextureCoordinates);
	//colour.r = colour.r * max(1.0f - maskColor.r, 0.1f);
	//colour.g = colour.g * max(1.0f - maskColor.g, 0.1f);
	//colour.b = colour.b * max(1.0f - maskColor.b, 0.1f);
	//colour.a = initialAlpha;
    return colour;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};