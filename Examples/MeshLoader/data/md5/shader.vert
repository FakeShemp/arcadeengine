#version 130

uniform mat4	mvpMatrix;		// ModelViewProjection matrix
uniform mat4	mvMatrix;		// ModelView matrix
uniform mat3	normalMatrix;	// transpose of the inverse of the upper
								// leftmost 3x3 of gl_ModelViewMatrix

in vec3 in_position;
in vec3 in_normal;
in vec3 in_tangent;
in vec2 in_texcoord;

out vec2 vTex;					// Texture coordinate
out vec4 vColor;				// Color to fragment program


void main(void) 
{ 

	// Get surface normal in eye coordinates
	vec3 vEyeNormal = (mvMatrix * vec4(in_Normal, 0.0)).xyz;

	mat3 mNormalMatrix;
	mNormalMatrix[0] = normalize(mvMatrix[0].xyz);
	mNormalMatrix[1] = normalize(mvMatrix[1].xyz);
	mNormalMatrix[2] = normalize(mvMatrix[2].xyz);
	vec3 vNorm = normalize(mNormalMatrix * in_Normal);

	vec4 ecPosition;
	vec3 ecPosition3;
	ecPosition = mvMatrix * vec4(in_position, 1.0);
	ecPosition3 = ecPosition.xyz /ecPosition.w;
	vec3 vLightDir = normalize(lightPosition.xyz - ecPosition3);

	float fDot = max(0.0, dot(vNorm, vLightDir)); 

	vColor.rgb = color.rgb * fDot;
	vColor.a = color.a;

	vTex = in_texcoord;
	gl_Position = mvpMatrix * vec4(in_position, 1.0); 
}


