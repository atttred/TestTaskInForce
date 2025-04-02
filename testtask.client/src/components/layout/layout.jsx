import { Outlet, Link } from 'react-router-dom';

const Layout = () => (
    <div>
        <nav>
            <Link to="/about">About</Link> | <Link to="/urls">URLs</Link> | <Link to="/auth">Auth</Link>
        </nav>
        <Outlet />
    </div>
);

export default Layout;