import axios from 'axios';
import { Account } from './types';
import { coreAppUrl, setURL } from '../config';

export class accountsApi {
    public getAll() {
        setURL(coreAppUrl);
        let token = localStorage.getItem("token");
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        return axios.get<Account[]>(`/Account/my`).then(res => res.data);
    }
    public get(id: string) {
        setURL(coreAppUrl);
        return axios.get<Account>(`/Account/${id}`).then(res => res.data);
    }
    public post(account: { type: number; currencyName: string; userId: string }) {
        setURL(coreAppUrl);
        return axios
            .post(`/Account?userId=${account.userId}`, {
                type: Number(account.type),
                currencyName: account.currencyName,
            })
            .then(res => res.data);
    }
    public delete(id: string) {
        setURL(coreAppUrl);
        return axios.delete(`/Account/${id}`).then(res => res.data);
    }
}
