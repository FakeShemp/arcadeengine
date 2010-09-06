#version 130

uniform mat4	mvpMatrix;
uniform mat4	mvMatrix;
uniform mat3	normalMatrix;
uniform vec4	diffuseColor;	
uniform vec3	lightPosition;


in vec3 in_position;
in vec3 in_Normal;
in vec2 in_texcoord;

out vec2 vTex;
smooth out vec4 vColor;		// Color to fragment program


void main(void) 
{ 

    // Get surface normal in eye coordinates
    vec3 vEyeNormal = normalMatrix * in_Normal;

    // Get vertex position in eye coordinates
    vec4 vPosition4 = mvMatrix * vec4(in_position, 1.0);
    vec3 vPosition3 = vPosition4.xyz / vPosition4.w;

    // Get vector to light source
    vec3 vLightDir = normalize(lightPosition - vPosition3);

    // Dot product gives us diffuse intensity
    float diff = max(0.0, dot(vEyeNormal, vLightDir));

    // Multiply intensity by diffuse color
    vColor.rgb = diff * diffuseColor.rgb;
    vColor.a = diffuseColor.a;


	vTex = in_texcoord;
	gl_Position = mvpMatrix * vec4(in_position, 1.0); 
}


