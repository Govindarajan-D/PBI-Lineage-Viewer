import { defineConfig } from 'vite'
import { svelte } from '@sveltejs/vite-plugin-svelte'
import svgr from 'vite-plugin-svelte-svgr';
import { viteSingleFile } from 'vite-plugin-singlefile'
import tailwindcss from '@tailwindcss/vite'

export default defineConfig({
  plugins: [
    svelte(),
    svgr(),
    viteSingleFile(),
    tailwindcss(),
  ],
  build: {
    target: 'esnext', // Ensure modern JavaScript is used
    assetsInlineLimit: Infinity, // Inline all assets
    cssCodeSplit: false, // Prevent CSS from being split into separate files
  },
})