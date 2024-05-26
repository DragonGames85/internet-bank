'use client';
import axios from 'axios';
import {
    CategoryScale,
    ChartData,
    Chart as ChartJS,
    ChartOptions,
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
import moment from 'moment';
ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend);

export default function Home() {
    const [responseTimes, setResponseTimes] = useState<number[]>([]);

    const [body, setBody] = useState<GLOBAL_API['/monitoring/api/all/tracing']['body'][]>();

    const [begin, setBegin] = useState(moment().add(-1, 'days').format('YYYY-MM-DD'));
    const [end, setEnd] = useState(moment().add(1, 'days').format('YYYY-MM-DD'));

    const optionsSTABILITY: ChartOptions<'line'> = {
        responsive: true,
        parsing: {
            xAxisKey: 'created_At',
            yAxisKey: 'statusCode',
        },
        plugins: {
            legend: {
                position: 'top' as const,
            },
            tooltip: {
                callbacks: {
                    title(tooltipItems) {
                        return tooltipItems[0].label;
                    },
                    label: function (context) {
                        const item = context.raw as GLOBAL_API['/monitoring/api/all/tracing']['body'];
                        return [
                            `ЗАПРОС: ${item.method + ' ' + item.route}`,
                            `СТАТУС КОД: ${item.statusCode}`,
                            `ОПИСАНИЕ: ${item.description}`,
                            `ТИП: ${item.type}`,
                            `ВРЕМЯ: ${item.time}`,
                            `СЕРВИС: ${item.service}`,
                        ];
                    },
                },
            },
            title: {
                display: true,
                text: 'График стабильности',
            },
        },
    };

    function timeStringToMilliseconds(timeString: string) {
        // Регулярное выражение для разбора строки времени
        const regex = /(\d{2}):(\d{2}):(\d{2})\.(\d{7})/;
        const match = timeString.match(regex);

        if (match) {
            const hours = parseInt(match[1], 10);
            const minutes = parseInt(match[2], 10);
            const seconds = parseInt(match[3], 10);
            const milliseconds = parseInt(match[4].slice(0, 3), 10); // берем первые три цифры для миллисекунд

            // Преобразование в миллисекунды
            return (hours * 3600 + minutes * 60 + seconds) * 1000 + milliseconds;
        } else {
            throw new Error('Invalid time string format');
        }
    }

    const dataSTABILITY: ChartData = {
        labels: responseTimes,
        datasets: [
            {
                label: 'Статус код запроса',
                data: body?.map((zapros, i) => ({
                    ...zapros,
                    time: timeStringToMilliseconds(zapros.time as string),
                    created_At: moment(zapros.created_At).format('YYYY-MM-DD HH:mm:ss'),
                })) as any,
                borderColor: 'rgb(255, 99, 132)',
                backgroundColor: 'rgba(255, 99, 132, 0.5)',
            },
        ],
    };

    const dataTIMERY: ChartData = {
        labels: responseTimes,
        datasets: [
            {
                label: 'Время выполнения запроса',
                data: body?.map((zapros, i) => ({
                    ...zapros,
                    time: timeStringToMilliseconds(zapros.time as string),
                    created_At: moment(zapros.created_At).format('YYYY-MM-DD HH:mm:ss'),
                })) as any,
                borderColor: 'rgb(0, 99, 255)',
                backgroundColor: 'rgba(0, 99, 255, 0.5)',
            },
        ],
    };

    const optionsTIMERY: ChartOptions<'line'> = {
        responsive: true,
        parsing: {
            xAxisKey: 'created_At',
            yAxisKey: 'time',
        },
        plugins: {
            legend: {
                position: 'top' as const,
            },
            tooltip: {
                callbacks: {
                    title(tooltipItems) {
                        return tooltipItems[0].label;
                    },
                    label: function (context) {
                        const item = context.raw as GLOBAL_API['/monitoring/api/all/tracing']['body'];
                        return [
                            `ЗАПРОС: ${item.method + ' ' + item.route}`,
                            `СТАТУС КОД: ${item.statusCode}`,
                            `ОПИСАНИЕ: ${item.description}`,
                            `ТИП: ${item.type}`,
                            `ВРЕМЯ: ${item.time}`,
                            `СЕРВИС: ${item.service}`,
                        ];
                    },
                },
            },
            title: {
                display: true,
                text: 'График времени запроса',
            },
        },
    };

    const getAllTracing = async (data: GLOBAL_API['/monitoring/api/all/tracing']['parameters']) => {
        try {
            const result = await axios.get<GLOBAL_API['/monitoring/api/all/tracing']['body'][]>(
                'https://bayanshonhodoev.ru/monitoring' + '/api/all/tracing',
                {
                    params: data,
                }
            );
            setBody(result.data);
            setResponseTimes(
                Array.from({ length: result.data.length }).map((_, i) =>
                    moment(result.data[i].created_At).format('YYYY-MM-DD HH:mm:ss')
                ) as any
            );
        } catch (error) {}
    };

    return (
        <main className="flex min-h-screen flex-col items-center justify-between px-12 py-2">
            <div className="flex items-center justify-center">
                <label>begin</label>
                <input
                    type="date"
                    defaultValue={moment().add(-1, 'days').format('YYYY-MM-DD')}
                    className="mb-2"
                    onChange={e => setBegin(e.target.value)}
                />
                <label>end</label>
                <input
                    type="date"
                    defaultValue={moment().add(1, 'days').format('YYYY-MM-DD')}
                    onChange={e => setEnd(e.target.value)}
                />
                <button
                    onClick={() => getAllTracing({ begin, end })}
                    className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
                >
                    Получить
                </button>
            </div>
            <Line options={optionsSTABILITY} data={dataSTABILITY} />
            <Line options={optionsTIMERY} data={dataTIMERY} />

            <ToastContainer />
            {/* <div className="flex flex-col gap-6">
                {body?.map(zapros => (
                    <div>
                        <p>Сделан: {zapros.created_At}</p>
                        <p>Запрос: {zapros.method + ' ' + zapros.route}</p>
                        <p>Сервис: {zapros.service}</p>
                        <p>СТАТУС КОД: {zapros.statusCode}</p>
                        <p>Описание: {zapros.description}</p>
                        <p>Тип: {zapros.type}</p>
                        <p>Время: {zapros.time}</p>
                    </div>
                ))}
            </div> */}
        </main>
    );
}
