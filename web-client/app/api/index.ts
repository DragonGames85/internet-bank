import { accountsApi } from './accounts';
import { creditsApi } from './credits';
import { operationsApi } from './operations';
import { authApi } from './auth';

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
    }
}

export const api = new Api();
