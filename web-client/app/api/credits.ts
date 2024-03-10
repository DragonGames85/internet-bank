import axios from 'axios';
import { Credit } from './types';

export class creditsApi {
    public getAll() {
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
        return axios.post('/addCredit', credit).then(res => res.data);
    }
    public close(id: string) {
        return axios.put(`/closeCredit/${id}`).then(res => res.data);
    }
}
