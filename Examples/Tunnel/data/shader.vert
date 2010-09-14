// Taken from http://www.rozengain.com/files/webgl/tunnel/

#version 330

in vec3 aVertexPosition;		// -- the vertex position (x, y, z)
in vec4 aVertexColor;			// -- the vertex color (r, g, b, a)
in vec2 aTextureCoord;			// -- the texture coordinate for this vertex (u, v)

uniform mat4 uMVMatrix;				// -- model-view matrix
uniform mat4 uPMatrix;				// -- projection matrix
uniform float fTime;				// -- the time value (changes every frame)


out vec4 vColor;			// -- the color
out vec2 vTextureCoord;		// -- the texture coordinates


void main(void) 
{
    vec3 pos = aVertexPosition;
    // -- displace the x coordinate based on the time and the z position 
	pos.x += cos(fTime + (aVertexPosition.z / 4.0));

    // -- displace the y coordinate based on the time and the z position 
	pos.y += sin(fTime + (aVertexPosition.z / 4.0));

    // -- transform the vertex 
	gl_Position = uPMatrix * uMVMatrix * vec4(pos, 1.0);

    // -- copy the vertex color
	vColor = aVertexColor;

    // -- displace the texture's y (v) coordinate. This gives the illusion of movement.
	vec2 texcoord = aTextureCoord;
    //texcoord.y = texcoord.y + (fTime);

    // -- copy the texture coordinate 
	vTextureCoord = texcoord;
}

