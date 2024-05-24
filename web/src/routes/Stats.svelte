<script lang="ts">
  export let serverEnv: string;
  const webEnv = import.meta.env.MODE;
</script>

<section class="fixed bottom-0 right-5 flex flex-col">
  <div class="flex">
    <span class="mr-2">web:</span>
    <span class="ml-auto">{webEnv}</span>
  </div>
  <div class="flex">
    <span class="mr-2">api:</span>
    <span class="ml-auto">{serverEnv}</span>
  </div>
</section>
