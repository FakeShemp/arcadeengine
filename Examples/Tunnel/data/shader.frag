// Taken from http://www.rozengain.com/files/webgl/tunnel/

#version 330

uniform sampler2D uSampler;

in vec4 vColor;
in vec2 vTextureCoord;

out vec4 FragColor;

void main(void)
{
	// -- get the pixel from the texture
	vec4 textureColor = texture2D(uSampler, vec2(vTextureCoord.s, vTextureCoord.t));
	
	// -- multiply the texture pixel with the vertex color
	FragColor = vColor * textureColor;
}
