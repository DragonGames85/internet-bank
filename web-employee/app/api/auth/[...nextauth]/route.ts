import NextAuth from 'next-auth';
import CredentialsProvider from 'next-auth/providers/credentials';
import GithubProvider from 'next-auth/providers/github';
import { api } from '../..';

const handler = NextAuth({
    providers: [
        CredentialsProvider({
            name: 'Логин + пароль',
            credentials: {
                login: { label: 'Логин', type: 'text', placeholder: 'User' },
                password: { label: 'Пароль', type: 'password' },
            },
            async authorize(credentials, req) {
                const user = await api.auth.login(credentials ? credentials : { login: '', password: '' });

                if (user) {
                    return user;
                } else {
                    return null;
                }
            },
        }),
        GithubProvider({
            clientId: 'fbfa25240b6e7bb41360',
            clientSecret: '01c78a27d49a0b162beae18ac8dcbd059819a268',
        }),
    ],
    callbacks: {
        async redirect({ url, baseUrl }) {
            if (url.startsWith('/')) return `${baseUrl}${url}`;
            else if (new URL(url).origin === baseUrl) return url;
            return baseUrl;
        },
        session: ({ session, token }) => {
            return {
                ...session,
                user: {
                    ...session.user,
                    id: token.userId,
                    userId: token.userId,
                    token: token.token,
                },
            };
        },
        jwt: ({ token, user }) => {
            if (user) {
                const u = user as unknown as any;
                return {
                    ...token,
                    id: u.userId,
                    userId: u.userId,
                    token: u.token,
                };
            }
            return token;
        },
    },
});

export { handler as GET, handler as POST };
// ? { id: '1', name: 'J Smith', email: 'jsmith@example.com' }
