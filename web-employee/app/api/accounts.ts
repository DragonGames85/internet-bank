import axios from 'axios';
import { Account } from './types';
import { setURL } from '../config';

export class accountsApi {
    public account(accountId: string) {
        setURL(7301);
        return axios.get<Account>(`/Account/${accountId}`).then(res => res.data);
    }
    public userAccounts(userId: string) {
        setURL(7301);
        return axios.get<Account[]>(`/Account/user/${userId}`).then(res => res.data);
    }
}
