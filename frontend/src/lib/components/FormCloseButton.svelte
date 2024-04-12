<script lang="ts">
  export let to: string;
</script>

<a href={to} class="close hover:scale-125 transition duration-100 ease-in-out" tabindex="0"
   role="button">close</a>

<style>
    .close {
        position: absolute;
        top: 0;
        right: 0;
        display: block;
        width: 40px;
        height: 40px;
        font-size: 0;
    }

    .close:before,
    .close:after {
        position: absolute;
        top: 50%;
        left: 50%;
        width: 5px;
        height: 20px;
        background-color: black;
        transform: rotate(45deg) translate(-50%, -50%);
        transform-origin: top left;
        content: '';
    }

    .close:after {
        transform: rotate(-45deg) translate(-50%, -50%);
    }
</style>
