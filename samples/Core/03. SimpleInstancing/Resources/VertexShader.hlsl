struct VOut
{
	float4 position : SV_POSITION;
	float4 color : COLOR;
};

struct VIn
{
	// standard vertex elements
	float4 position: POSITION;
	float4 color: COLOR;

	// instance elements
	float4x4 translation: CUSTOM;
};

VOut VSMain(VIn input)
{
	VOut output;
	output.position = mul(input.translation, input.position);
	output.color = input.color;
	return output;
}
