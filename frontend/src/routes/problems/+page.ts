export const load = async ({ fetch }) => {
	const url = '/api/problems';
	const res = await fetch(url);
	console.log(res);
};
