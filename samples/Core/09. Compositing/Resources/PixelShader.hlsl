Texture2D ShaderTexture0 : register(t0);
Texture2D ShaderTexture1 : register(t1);
Texture2D ShaderTexture2 : register(t2);
Texture2D ShaderTexture3 : register(t3);
Texture2D ShaderTexture4 : register(t4);

SamplerState Sampler : register(s0);

struct VOut
{
	float4 position : SV_POSITION;
	float2 texcoord: TEXCOORD;
	int tex: CUSTOM;
};

float4 PSMain(VOut input) : SV_Target
{
	float4 color;
	[flatten]
	switch (input.tex)
	{
	case 1:
		color = ShaderTexture1.Sample(Sampler, input.texcoord);
		break;
	case 2:
		color = ShaderTexture2.Sample(Sampler, input.texcoord);
		break;
	case 3:
		color = ShaderTexture3.Sample(Sampler, input.texcoord);
		break;
	case 4:
		color = ShaderTexture4.Sample(Sampler, input.texcoord);
		break;
	default:
		color = ShaderTexture0.Sample(Sampler, input.texcoord);
		break;
	}
	return color;
}