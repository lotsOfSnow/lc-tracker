<script lang="ts">
  import '../app.css';
  import { AppRoute } from '$lib/routes';
  import ToastArea from '$lib/components/notifications/ToastArea.svelte';
  import { apiClient } from '$lib/api';
  import Button from '$lib/components/Button.svelte';
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

  const onExportClick = async () => {
    const result = await apiClient.GET('/api/me/export', { parseAs: 'blob' });

    if (!result.data) {
      addToast('error', 'Failed to export data');
      return;
    }

    const a = document.createElement('a');
    a.href = window.URL.createObjectURL(result.data);
    a.download = 'exported.json';
    a.click();
  };
</script>

<ToastArea />

<div class="flex h-screen">
  <!-- Sidebar -->
  <div class="bg-gray-800 text-white w-64 flex-shrink-0 flex flex-col">
    <nav class="p-4 flex-grow">
      <ul>
        {#each items as item}
          <li class="mb-2">
            <a href="{item.link}"
               class="block py-2 px-4 bg-gray-700 hover:bg-gray-600 transition duration-300 ease-in-out rounded-lg">{item.text}</a>
          </li>
        {/each}
      </ul>
    </nav>
    <div class="flex justify-center">
      <form>
        <Button type="submit" class="p-6 mb-5"
                on:click={onExportClick}>
          Export
          data
        </Button>
      </form>
    </div>
  </div>

  <!-- Content -->
  <div class="flex-1 overflow-hidden">
    <div class="px-6 py-4">
      <slot />
    </div>
  </div>
</div>
