<script lang="ts">
  import '../app.css';
  import { AppRoute } from '$lib/routes';
  import ToastArea from '$lib/components/notifications/ToastArea.svelte';
  import { addToast } from '$lib/components/notifications/toastStore';

  interface Item {
    text: string,
    link: string,
  }

  const items: Item[] = [
    {
      text: 'Home',
      link: AppRoute.HOME,
    },
    {
      text: 'Problems',
      link: AppRoute.PROBLEMS,
    },
    {
      text: 'Attempts',
      link: AppRoute.ATTEMPTS,
    },
  ];
</script>

<ToastArea />

<div class="flex h-screen">
  <button on:click={() => addToast('success', false, 'Test', 1000)}>Click</button>

  <!-- Sidebar -->
  <div class="bg-gray-800 text-white w-64 flex-shrink-0">
    <nav class="p-4">
      <ul>
        {#each items as item}
          <li class="mb-2">
            <a href="{item.link}"
               class="block py-2 px-4 bg-gray-700 hover:bg-gray-600 transition duration-300 ease-in-out rounded-lg">{item.text}</a>
          </li>
        {/each}
      </ul>
    </nav>
  </div>

  <!-- Content -->
  <div class="flex-1 overflow-hidden">
    <div class="px-6 py-4">
      <slot />
    </div>
  </div>
</div>
