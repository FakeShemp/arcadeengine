#version 130

uniform mat4 projection_matrix;			// Projection matrix
uniform mat4 texture_matrix;			// Texture matrix

in vec3 in_position;					// Vertex coordinate
in vec4 in_color;						// Color
in vec2 in_texture;						// Texture coordinate
 
out vec2 out_texture;					// Texture coordinate
out vec4 out_color;						// Fragment color
 
void main()
{
	// Position
	gl_Position = projection_matrix * vec4(in_position, 1.0);

	// Texture coordinate
	out_texture = vec2(texture_matrix * vec4(in_texture, 0.0, 1.0));

	// Fragment color
	out_color = in_color;
}