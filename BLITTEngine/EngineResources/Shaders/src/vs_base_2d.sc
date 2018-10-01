$input a_position, a_texcoord0, a_color0 
$output v_color0, v_texcoord0

#include "../Common/common.sh"

void main()
{

	v_color0 = a_color0;
	v_texcoord0 = a_texcoord0;
	gl_Position = mul(u_modelViewProj, vec4(a_position, 0.0, 1.0));

}