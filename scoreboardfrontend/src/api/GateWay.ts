import axios, { AxiosInstance } from "axios";

class ApiCaller {
	port: string;
	baseURL: string;
	axiosInstance: AxiosInstance;

	constructor(gwPort: string) {
		this.port = gwPort;
		this.baseURL = `http://localhost:${this.port}/score`;
		this.axiosInstance = axios;
		this.axiosInstance.defaults.baseURL = this.baseURL;
	}
	async get(path: string) {
		console.log(localStorage.getItem('accessToken'));
			if (localStorage.getItem('accessToken')) {
				const headers = {
					Accept: 'application/json',
					Authorization: 'Bearer ' + localStorage.getItem('accessToken')
				};
				let response = this.axiosInstance.get(path, { headers });

				return response;
			}
			else {
				let response = this.axiosInstance.get(path);
				console.log("NotAuthorizing");

				return response;
			}
	}
}
export default ApiCaller;