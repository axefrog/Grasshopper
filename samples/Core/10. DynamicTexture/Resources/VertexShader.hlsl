struct VOut
{
    float4 position : SV_POSITION;
    float4 color : COLOR;
    float2 texcoord: TEXCOORD;
};

struct VIn
{
    float4 position: POSITION;
    float4 color: COLOR;
    float2 texcoord: TEXCOORD;
};

VOut VSMain(VIn input)
{
    VOut output;
    output.position = input.position;
    output.color = input.color;
    output.texcoord = input.texcoord;
    return output;
}
