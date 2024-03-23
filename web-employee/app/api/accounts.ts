import axios from 'axios';
import { Account } from './types';
import { coreAppUrl, setURL } from '../config';

export class accountsApi {
    public account(accountId: string) {
        setURL(coreAppUrl);
        return axios.get<Account>(`/Account/${accountId}`).then(res => res.data);
    }
    public userAccounts(userId: string) {
        setURL(coreAppUrl);
        return axios.get<Account[]>(`/Account/user/${userId}`).then(res => res.data);
    }
}
