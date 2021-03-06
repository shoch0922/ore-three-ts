varying vec2 vUv;
uniform sampler2D backbuffer;
uniform vec2 resolution;

uniform bool v;
uniform float gaussVar;
uniform sampler2D sceneTex;

#define PI 3.14159265
#define SAMPLES 32

float gaussian( float x, float v ) {
    return 1.0 / sqrt( 2.0 * PI * v ) * exp( - x * x / 2.0 / v );
}

void main(){
	vec2 bv = ( v ? vec2( 0.0, 1.0 ) : vec2( 1.0, 0.0 ) ) / resolution;
	vec3 sum = vec3( 0.0  );
	for( int i = 0; i < SAMPLES; i++ ){
		vec3 t1 = texture2D( backbuffer, vUv + bv * float(i) ).xyz;
		vec3 t2 = texture2D( backbuffer, vUv - bv * float(i) ).xyz;
		float mul = gaussian( float ( i ), gaussVar ); 
		sum += (t1 + t2) * mul * 1.0;
	}

	if(v){
		sum += texture2D( sceneTex, vUv ).xyz;
	}

	gl_FragColor = vec4( sum, 1.0);
}