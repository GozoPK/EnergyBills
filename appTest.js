import http from 'k6/http';

export let options = {
    maxRedirects: 0,
	stages: [
		{ duration: '10s', target: 100 },
		{ duration: '40s', target: 100 },
		{ duration: '10s', target: 0 }
	]
};

export default function() {
	http.get('http://192.168.1.8');
}
