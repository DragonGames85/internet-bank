'use client';
import axios from 'axios';
import {
    CategoryScale,
    ChartData,
    Chart as ChartJS,
    Legend,
    LineElement,
    LinearScale,
    PointElement,
    Title,
    Tooltip,
} from 'chart.js';
import { useState } from 'react';
import { Line } from 'react-chartjs-2';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { GLOBAL_API } from './api';
ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend);

export default function Home() {
    const [responseTimes, setResponseTimes] = useState<number[]>([]);

    const [body, setBody] = useState<GLOBAL_API['/monitoring/api/all/tracing']['body'][]>();

    const [begin, setBegin] = useState('');
    const [end, setEnd] = useState('');

    const options = {
        responsive: true,
        plugins: {
            legend: {
                position: 'top' as const,
            },
            title: {
                display: true,
                text: 'График стабильности',
            },
        },
    };


    const data: ChartData = {
        labels: responseTimes,
        datasets: [
            {
                label: 'СТАТУС КОД',
                data: body?.map(f => f.statusCode!)!,
                borderColor: 'rgb(255, 99, 132)',
                backgroundColor: 'rgba(255, 99, 132, 0.5)',
            },
        ],
    };

    const getAllTracing = async (data: GLOBAL_API['/monitoring/api/all/tracing']['parameters']) => {
        let result = null;
        try {
            result = await axios.get<GLOBAL_API['/monitoring/api/all/tracing']['body'][]>(
                'https://bayanshonhodoev.ru/monitoring' + '/api/all/tracing',
                {
                    params: data,
                }
            );
            setBody(result.data);
            setResponseTimes(Array.from({ 'length': result.data.length }).map((_, i) => i + 1))
        } catch (error) { }
    };

    // getAllTracing({});

    return (
        <main className="flex min-h-screen flex-col items-center justify-between px-12 py-2">
            <div className="flex items-center justify-center">
                <label>begin</label>
                <input type="date" className="mb-2" onChange={e => setBegin(e.target.value)} />
                <label>end</label>
                <input type="date" onChange={e => setEnd(e.target.value)} />
            </div>
            <Line options={{ ...options, }} data={data} />
            <button
                onClick={() => getAllTracing({ begin, end })}
                className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
            >
                Получить
            </button>
            <ToastContainer />
            <div className='flex flex-col gap-6'>
                {body?.map(zapros => <div>
                    <p>Сделан: {zapros.created_At}</p>
                    <p>Запрос: {zapros.method + ' ' + zapros.route}</p>
                    <p>Сервис: {zapros.service}</p>
                    <p>СТАТУС КОД: {zapros.statusCode}</p>
                    <p>Описание: {zapros.description}</p>
                    <p>Тип: {zapros.type}</p>
                    <p>Время: {zapros.time}</p>
                </div>)}
            </div>
        </main>
    );
}
