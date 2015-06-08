cbuffer ViewData : register(b0)
{
	float4x4 worldViewProjection;
}

struct VOut
{
	float4 position : SV_POSITION;
	float4 color : COLOR;
};

struct VIn
{
	float4 position: POSITION;
	float4 color: COLOR;
	float2 texcoord: TEXCOORD;
	float2 pad: PADDING0;
};

VOut VSMain(VIn input)
{
	VOut output;
	output.position = mul(input.position, worldViewProjection);
	output.color = input.color;
	return output;
}
