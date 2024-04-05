import Link from 'next/link';

const Login = () => (
    <div className="flex-center flex-col">
        <h1>ВЫ НЕ ВОШЛИ В СИСТЕМУ</h1>
        <Link href={`http://localhost:3002?home=localhost:3001`} passHref>
            <button className="border-2 rounded-full bg-bgColor2 dark:bg-bgColor3Dark text-3xl text-center self-center p-4 px-16">
                АВТОРИЗОВАТЬСЯ
            </button>
        </Link>
    </div>
);

export default Login;
