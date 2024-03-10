import axios from 'axios';
import { accountsApi } from './accounts';
import { creditsApi } from './credits';
import { operationsApi } from './operations';
import { usersApi } from './users';

class Api {
    public users: usersApi;
    public operations: operationsApi;
    public credits: creditsApi;
    public accounts: accountsApi;

    constructor() {
        this.users = new usersApi();
        this.operations = new operationsApi();
        this.credits = new creditsApi();
        this.accounts = new accountsApi();
    }
}

axios.defaults.baseURL = 'https://localhost:7227/api';

export const api = new Api();
