import axios from 'axios';
import { Account } from './types';

export class accountsApi {
    public getAll() {
        return axios.get<Account[]>(`/Account/my`).then(res => res.data);
    }
    public get(id: string) {
        return axios.get<Account>(`/Account/${id}`).then(res => res.data);
    }
    public post(account: { type: number; currencyName: string }) {
        return axios.post('/Account', account).then(res => res.data);
    }
    // public put(
    //     id: string,
    //     account: {
    //         number: string;
    //         balance: number;
    //         type: number;
    //         createdDate?: string;
    //         closedDate?: string;
    //         userId?: string;
    //         currencyName: string;
    //     }
    // ) {
    //     return axios.put(`/Account/${id}`, account).then(res => res.data);
    // }
    public delete(id: string) {
        return axios.delete(`/Account/${id}`).then(res => res.data);
    }
}
