import axios from 'axios';
import { Credit } from './types';

export class creditsApi {
    public getAll() {
        return axios.get('/getTarrifs').then(res => res.data);
    }
    public get(userId: string) {
        return axios.get(`/getUserCredits/${userId}`).then(res => res.data);
    }
    public post(credit: Omit<Credit, 'id'> & Partial<Credit>) {
        return axios.post('/createCreditTariff', credit).then(res => res.data);
    }
    // public put(id: string, credit: Omit<Credit, 'id'> & Partial<Credit>) {
    //     return axios.put(`/credits/${id}`, credit).then(res => res.data);
    // }
    // public delete(id: string) {
    //     return axios.delete(`/credits/${id}`).then(res => res.data);
    // }
}
