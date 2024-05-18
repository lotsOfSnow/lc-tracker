<script>
  import { dismissToast, toasts } from '$lib/components/notifications/toastStore';
  import Toast from '$lib/components/notifications/Toast.svelte';
</script>

{#if $toasts}
  <section class="fixed bottom-10 right-0 z-[1000] w-[20vw]">
    {#each $toasts as toast (toast.id)}
      <Toast
        type={toast.type}
        dismissible={toast.dismissible}
        on:dismiss={() => dismissToast(toast.id)}>{toast.message}</Toast>
    {/each}
  </section>
{/if}
