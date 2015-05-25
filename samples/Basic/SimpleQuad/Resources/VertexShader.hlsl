struct VOut
{
	float4 position : SV_POSITION;
	float4 color : COLOR;
};

struct VIn
{
	float4 position: POSITION0;
	float4 color: COLOR0;
	float2 texcoord: TEXCOORD0;
};

VOut VSMain(VIn input)
{
	VOut output;
	output.position = input.position;
	output.color = input.color;
	return output;
}
