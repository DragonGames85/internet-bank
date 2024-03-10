import axios from 'axios';
import { Account } from './types';

export class accountsApi {
    public get(userId: string) {
        return axios.get<Account>(`/Account/${userId}`).then(res => res.data);
    }
}
