import axios from 'axios';
import { Credit } from './types';
import { creditAppUrl, setURL } from '../config';

export class creditsApi {
    public getAll() {
        setURL(creditAppUrl);
        return axios.get<Credit[]>('/getTarrifs').then(res => res.data);
    }
    public sub(credit: {
        userId: string;
        currency: string;
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
}
