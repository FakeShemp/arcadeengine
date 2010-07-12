#version 130

uniform mat4 modelview_matrix;
uniform mat4 projection_matrix;
uniform mat4 mvp_matrix;
uniform mat4 shadowMatrix;

in vec3 in_position;
in vec3 in_normal;
in vec3 in_tangent;
in vec2 in_texcoord;

out vec4 out_color;

void main(void)
{
	gl_Position = mvp_matrix * vec4(in_position, 1.0);
					
	out_color = modelview_matrix * vec4(in_normal, 1.0);
}
