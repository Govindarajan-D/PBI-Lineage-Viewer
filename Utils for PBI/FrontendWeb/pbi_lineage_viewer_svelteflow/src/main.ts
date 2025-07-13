import { mount } from 'svelte';
import App from './App.svelte';

import './css/index.css';

mount(App, {
  target: document.getElementById('app')!,
});
