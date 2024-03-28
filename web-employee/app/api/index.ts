import axios from 'axios';
import { accountsApi } from './accounts';
import { creditsApi } from './credits';
import { operationsApi } from './operations';
import { usersApi } from './users';
import { authApi } from './auth';

class Api {
    public users: usersApi;
    public operations: operationsApi;
    public credits: creditsApi;
    public accounts: accountsApi;
    public auth: authApi;

    constructor() {
        this.users = new usersApi();
        this.operations = new operationsApi();
        this.credits = new creditsApi();
        this.accounts = new accountsApi();
        this.auth = new authApi();

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

axios.defaults.baseURL = 'https://localhost:7227/api';

export const api = new Api();
