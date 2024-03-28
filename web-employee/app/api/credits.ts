import axios from 'axios';
import { Credit } from './types';
import { creditAppUrl, setURL } from '../config';

export class creditsApi {
    public getAll() {
        setURL(creditAppUrl);
        return axios.get<Credit[]>('/getTarrifs').then(res => res.data);
    }
    public get(id: string) {
        setURL(creditAppUrl);
        return axios.get<Credit>(`/getCredit/${id}`).then(res => res.data);
    }
    public getUserCredits(userId: string) {
        setURL(creditAppUrl);
        return axios.get<Credit[]>(`/getUserCredits/${userId}`).then(res => res.data);
    }
    public post(credit: Omit<Credit, 'id'> & Partial<Credit>) {
        setURL(creditAppUrl);
        return axios.post('/createCreditTariff', credit).then(res => res.data);
    }
    public delete(id: string) {
        setURL(creditAppUrl);
        return axios.delete(`/deleteCredit/${id}`).then(res => res.data);
    }
    // * Просроченные кредиты
    public expired(userId: string) {
        setURL(creditAppUrl);
        return axios.get<Credit[]>(`/getUserCredits/${userId}`).then(res => res.data);
    }
}
