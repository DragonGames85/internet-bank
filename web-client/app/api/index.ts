import { accountsApi } from './accounts';
import { creditsApi } from './credits';
import { operationsApi } from './operations';
import { authApi } from './auth';
import axios from 'axios';

class Api {
    public auth: authApi;
    public operations: operationsApi;
    public credits: creditsApi;
    public accounts: accountsApi;

    constructor() {
        this.auth = new authApi();
        this.operations = new operationsApi();
        this.credits = new creditsApi();
        this.accounts = new accountsApi();

        axios.interceptors.response.use(
            response => {
                return response;
            },
            error => {
                const URL = error.request.responseURL;
                console.log(URL, error);
            }
        );
    }
}

export const api = new Api();
