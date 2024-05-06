<script lang="ts">
  export let width: 'small' | 'big' = 'small';

  const getMaxWidth = () => {
    switch (width) {
      case 'big':
        return 'max-w-6xl';
      default:
        return 'max-w-md';
    }
  };
</script>

<div class={`bg-gray-100 p-6 w-full ${getMaxWidth()} mx-auto rounded-lg shadow-md relative`}>
  <slot />
</div>
