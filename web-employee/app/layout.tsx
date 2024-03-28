import type { Metadata } from 'next';
import { Roboto } from 'next/font/google';
import './globals.css';
import { HeaderComp } from './components/Header';
import { ThemeProvider, AuthSessionProvider } from './providers';

const roboto = Roboto({ weight: '400', subsets: ['latin'], display: 'swap' });

export const metadata: Metadata = {
    title: 'WEB-EMPLOYEE',
};

export default function RootLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <AuthSessionProvider>
            <html lang="ru">
                <body className={roboto.className + ' min-h-screen'}>
                    <ThemeProvider attribute="class" defaultTheme="system" enableSystem>
                        <HeaderComp />
                        <main className={`px-2 sm:px-6 md:px-12 lg:px-24 xl:px-36 2xl:px-48 pt-24`}>{children}</main>
                    </ThemeProvider>
                </body>
            </html>
        </AuthSessionProvider>
    );
}
