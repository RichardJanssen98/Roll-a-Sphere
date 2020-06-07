import axios, { AxiosInstance } from "axios";
import Axios from "axios";
import { awaitExpression } from "@babel/types";

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
		let response = await this.axiosInstance.get(path);

		return response;
	}
}
export default ApiCaller;