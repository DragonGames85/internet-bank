import axios from 'axios';
import { creditAppUrl, setURL } from '../config';
import { Credit, CurrencySymbol } from './types';

export class creditsApi {
    public getAll() {
        setURL(creditAppUrl);
        return axios.get<Credit[]>('/getTariffs').then(res => res.data);
    }

    public sub(credit: {
        userId: string;
        currency: CurrencySymbol;
        value: number;
        tariffId: string;
        paymentPeriod: number;
        repaymentPeriod: number;
    }) {
        setURL(creditAppUrl);
        return axios.post('/takeCredit', credit).then(res => res.data);
    }
    public close(id: string) {
        setURL(creditAppUrl);
        return axios.put(`/closeCredit/${id}`).then(res => res.data);
    }
    public getUserCredits(userId: string) {
        setURL(creditAppUrl);
        return axios.get<Credit[]>(`/getUserCredits/${userId}`).then(res => res.data);
    }
    public expired(userId: string) {
        setURL(creditAppUrl);
        return axios.get<Credit[]>(`/overdueLoans/${userId}`).then(res => res.data);
    }
}
