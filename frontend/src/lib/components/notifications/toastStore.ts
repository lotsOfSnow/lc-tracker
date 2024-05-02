import { writable } from 'svelte/store';
import type { ToastType } from '$lib/components/notifications/toastType';

export const toasts = writable<ToastData[]>([]);

const defaultToastTimeout = 5000;

export const addToast = (
  type: ToastType,
  message: string,
  dismissible: boolean = true,
  timeout: number | undefined = defaultToastTimeout,
) => {
  const id = Math.floor(Math.random() * 1000);

  const data: ToastData = {
    id,
    type,
    dismissible,
    timeout,
    message,
  };

  toasts.update((all) => [{ ...data }, ...all]);

  if (data.timeout) {
    setTimeout(() => dismissToast(id), data.timeout);
  }
};

export const dismissToast = (id: number) => {
  toasts.update((all) => all.filter((value) => value.id !== id));
};

export type ToastData = {
  id: number;
  type: ToastType;
  dismissible: boolean;
  timeout?: number;
  message: string;
};
