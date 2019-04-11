$input v_color0, v_texcoord0

#include "common.sh"

SAMPLER2D(texture_2d, 0);

uniform vec4 params;

#define amount params.x
#define intensity params.y
#define time params.z


void main()
{
    float pos0 = ((v_texcoord0.y + 1.0) * 170.0 * amount);
    float pos1 = cos((fract(pos0) - 0.5) * 3.1415926 * intensity) * 1.5;
    vec4 rgb = texture2D(texture_2d, v_texcoord0);
      
    // slight contrast curve
    vec4 color = rgb*0.5+0.5*rgb*rgb*1.2;
      
    // color tint
    color *= vec4(0.9,1.0,0.7, 1.0);
      
    // vignette
    color *= 1.1 - 0.6 * (dot(v_texcoord0 - 0.5, v_texcoord0 - 0.5) * 2.0);
    
    gl_FragColor = mix(vec4(0,0,0,0), color, pos1);
    
    
}