Texture2D ShaderTexture : register(t0);
SamplerState Sampler : register(s0);

struct VOut
{
	float4 position : SV_POSITION;
	float2 texcoord: TEXCOORD;
};

float4 PSMain(VOut input) : SV_Target
{
	return ShaderTexture.Sample(Sampler, input.texcoord);
}