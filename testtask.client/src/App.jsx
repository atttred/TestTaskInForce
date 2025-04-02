import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Layout from './components/layout/layout';
import About from './components/about/about';
import Urls from './components/urls/urls';
import Auth from './components/auth/auth';

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route path="about" element={<About />} />
                    <Route path="urls" element={<Urls />} />
                    <Route path="auth" element={<Auth />} />
                    <Route index element={<About />} />
                </Route>
            </Routes>
        </BrowserRouter>
    );
}

export default App;