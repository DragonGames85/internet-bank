import axios from 'axios';

export class authApi {
    public login(user: { login: string; password: string }) {
        return axios.post<{ token: string }>('/auth/login', user).then(res => res.data);
    }
    public register(user: { login: string; name: string; confirmPassword: string; password: string }) {
        return axios.post<{ token: string }>('/auth/register', user).then(res => res.data);
    }
}
