#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

#define MAXLIGHT 20

float3 PointLightPosition[MAXLIGHT];
float4 PointLightColor[MAXLIGHT];
float PointLightIntensity[MAXLIGHT];
float PointLightRadius[MAXLIGHT];

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

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float4 screenColor = tex2D(ScreenTextureSampler, input.TextureCoordinates);
	float4 maskColor = tex2D(MaskTextureSampler, input.TextureCoordinates);
	screenColor.r = screenColor.r * max(1.0f - maskColor.r, 0.1f);
	screenColor.g = screenColor.g * max(1.0f - maskColor.g, 0.1f);
	screenColor.b = screenColor.b * max(1.0f - maskColor.b, 0.1f);
	return screenColor;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};