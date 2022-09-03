#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D ScreenTexture;

sampler2D ScreenTextureSampler = sampler_state
{
	Texture = <ScreenTexture>;
};

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
	return screenColor - maskColor;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};