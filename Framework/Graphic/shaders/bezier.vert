// A vertex shader to draw bezier curves
// GLSL version of the made by: Michael Dominic K. <mdk@mdk.am>
// Adaptation made by API <apinheiro@igalia.com>

attribute vec4 c1;
attribute vec4 c2;
attribute vec4 c3;
attribute vec4 c4;
attribute float width;
attribute float step;
attribute float lvl;
/* On the original segment was an int, here 
 * I can't use an int, as I get an
 * OpenGL does not allow attributes of type int
 */
attribute float segment;

void
main (void)
{
  vec4 current   = gl_Vertex;
  vec4 next      = gl_Vertex;
  vec4 tangent   = gl_Vertex;
  vec4 p_tangent = gl_Vertex;
  float p;
  float my_lvl = lvl;
  vec4 my_Vertex = gl_Vertex;/* I need a copy of this two, on GLSL I can't 
				write directly on atributes or built-ins  */

  current.x = (c1.x * (1.0 - my_lvl) * (1.0 - my_lvl) * (1.0 - my_lvl)) +
    (c2.x * 3.0 * my_lvl * (1.0 - my_lvl) * (1.0 - my_lvl)) +
    (c3.x * 3.0 * my_lvl * my_lvl * (1.0 - my_lvl)) +
    (c4.x * my_lvl * my_lvl * my_lvl);

  current.y = c1.y * (1.0 - my_lvl) * (1.0 - my_lvl) * (1.0 - my_lvl) +
    c2.y * 3.0 * my_lvl * (1.0 - my_lvl) * (1.0 - my_lvl) +
    c3.y * 3.0 * my_lvl * my_lvl * (1.0 - my_lvl) +
    c4.y * my_lvl * my_lvl * my_lvl;

  my_lvl += step;

  next.x = (c1.x * (1.0 - my_lvl) * (1.0 - my_lvl) * (1.0 - my_lvl)) +
    (c2.x * 3.0 * my_lvl * (1.0 - my_lvl) * (1.0 - my_lvl)) +
    (c3.x * 3.0 * my_lvl * my_lvl * (1.0 - my_lvl)) +
    (c4.x * my_lvl * my_lvl * my_lvl);

  next.y = c1.y * (1.0 - my_lvl) * (1.0 - my_lvl) * (1.0 - my_lvl) +
    c2.y * 3.0 * my_lvl * (1.0 - my_lvl) * (1.0 - my_lvl) +
    c3.y * 3.0 * my_lvl * my_lvl * (1.0 - my_lvl) +
    c4.y * my_lvl * my_lvl * my_lvl;

  tangent = next - current;
  tangent = normalize (tangent);
  p_tangent = tangent;
  p_tangent.x = - tangent.y;
  p_tangent.y = tangent.x;

  if (segment == 1.0) 
    my_Vertex = current + (p_tangent * width);
  else if (segment == 2.0) 
    my_Vertex = current - (p_tangent * width);
  else if (segment == 3.0) 
    my_Vertex = current - (tangent * width) + (p_tangent * width);
  else if (segment == 4.0) 
    my_Vertex = current - (tangent * width) - (p_tangent * width);
  else if (segment == 5.0) 
    my_Vertex = current + (tangent * width) + (p_tangent * width);
  else if (segment == 6.0) 
    my_Vertex = current + (tangent * width) - (p_tangent * width);

  gl_Position = gl_ModelViewProjectionMatrix * my_Vertex;
  gl_FrontColor = gl_Color;
}
