#version 130

in vec3 in_position;
in vec2 in_texture;
in vec4 in_color;
 
out vec2 out_texture;
out vec4 out_color;
 
uniform mat4 modelview_matrix;            
uniform mat4 projection_matrix;
 
void main()
{
	gl_Position = modelview_matrix * vec4(in_position,1.0);

	out_texture = in_texture;
	out_color = in_color;
}