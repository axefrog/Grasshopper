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
	[flatten]
	switch (input.tex)
	{
	case 1:
		return ShaderTexture1.Sample(Sampler, input.texcoord);
	case 2:
		return ShaderTexture2.Sample(Sampler, input.texcoord);
	case 3:
		return ShaderTexture3.Sample(Sampler, input.texcoord);
	case 4:
		return ShaderTexture4.Sample(Sampler, input.texcoord);
	default:
		return ShaderTexture0.Sample(Sampler, input.texcoord);
	}
}