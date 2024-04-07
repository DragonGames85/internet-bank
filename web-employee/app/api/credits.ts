import axios from 'axios';
import { Credit } from './types';
import { creditAppUrl, setURL } from '../config';

export class creditsApi {
    public getAll() {
        setURL(creditAppUrl);
        return axios.get<Credit[]>('/getTariffs').then(res => res.data);
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
        return axios
            .post('/createCreditTariff', {
                ...credit,
                paymentType: Number(credit.paymentType),
                rateType: Number(credit.rateType),
            })
            .then(res => res.data);
    }
    public delete(id: string) {
        setURL(creditAppUrl);
        return axios.delete(`/delete/${id}`).then(res => res.data);
    }
    public getUserCredRating(userId: string) {
        setURL(creditAppUrl);
        return axios.get<number>(`/rating/${userId}`).then(res => res.data);
    }
    // * Просроченные кредиты
    public expired(userId: string) {
        setURL(creditAppUrl);
        return axios.get<Credit[]>(`/overdueLoans/${userId}`).then(res => res.data);
    }
}
