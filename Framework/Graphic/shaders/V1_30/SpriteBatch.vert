#version 130

uniform mat4 modelview_matrix;            
uniform mat4 projection_matrix;
uniform mat4 texture_matrix;

in vec3 in_position;
in vec2 in_texture;
in vec4 in_color;
 
out vec4 out_texture;
out vec4 out_color;
 
void main()
{
	gl_Position = modelview_matrix * vec4(in_position, 1.0);

	out_texture = texture_matrix * vec4(in_texture, 0.0, 0.0);
	out_color = in_color;
}