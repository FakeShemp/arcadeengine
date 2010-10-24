#version 120

uniform sampler2D texture;		// Texture sampler

varying vec2 out_texture;			// Texture coordinate
varying vec4 out_color;				// Fragment color

void main()
{
	// Fragment color
	gl_FragColor = texture2D(texture, out_texture) * out_color;
}